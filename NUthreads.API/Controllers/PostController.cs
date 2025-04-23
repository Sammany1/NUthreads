using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NUthreads.Infrastructure.Repositories;

namespace NUthreads.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;
        public PostController(IPostRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (post == null) return BadRequest("Post cannot be null");
            await _repository.Create(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(string id)
        {
            var post = await _repository.GetById(id);
            if (post == null) return new NotFoundResult();
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] Post post)
        {
            if (post == null) return BadRequest("Post cannot be null");
            if (id != post.Id) return BadRequest("post");
            var existingPost = await _repository.GetById(id);
            if (existingPost == null) return NotFound("Post not found");
            await _repository.Update(post);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var post = await _repository.GetById(id);
            if (post == null) return NotFound("Post not found");
            await _repository.Delete(id);
            return Ok("Post Deleted");

        }
    }

}