using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<RatingController> _logger;

        public RatingController(IRatingService ratingService, IConnectionMultiplexer redis, ILogger<RatingController> logger)
        {
            _ratingService = ratingService;
            _redis = redis;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllRatings();
            return Ok(ratings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] Rating rating)
        {
            try
            {
                // Insert rating into MongoDB
                await _ratingService.InsertRating(rating);
                _logger.LogInformation($"Rating for user {rating.UsuarioId} and movie {rating.PeliculaId} inserted into MongoDB.");

                // Insert rating into Redis
                var db = _redis.GetDatabase();
                var redisKey = $"calificacion:{rating.UsuarioId}:{rating.PeliculaId}";
                var ratingData = JsonSerializer.Serialize(rating);
                await db.StringSetAsync(redisKey, ratingData);
                _logger.LogInformation($"Rating for user {rating.UsuarioId} and movie {rating.PeliculaId} inserted into Redis with key {redisKey}.");

                return Created("Created", true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating rating: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(string id, [FromBody] Rating rating)
        {
            try
            {
                rating.Id = id;
                await _ratingService.UpdateRating(rating);
                _logger.LogInformation($"Rating with ID {id} updated.");
                return Created("Created", true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating rating with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
