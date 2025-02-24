using AppApi.Data;
using AppApi.Model.Dto;
using AppApi.Model.Entities;
using AppApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService service;

        public PostsController(PostService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetAllPosts()
        {
            return Ok(service.GetAllPosts());
        }

        [HttpGet("{id:int}")]
        public ActionResult GetPost(int id)
        {
            var post = service.GetPost(id);
            if (post == null) 
            {
                return NotFound("Post not found!");
            }
            return Ok(post);
        }

        [HttpPost]
        public ActionResult<PostDto> CreatePost([FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post data is null.");
            }

            var post = service.CreatePost(postDto);

            return CreatedAtAction(nameof(GetPost), new { id = post.Value?.Id }, post.Value);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PostDto> UpdatePost(int id, [FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("Post data is null.");
            }

            var result = service.UpdatePost(id, postDto);

            if (result == null)
            {
                return NotFound("Post not found.");
            }

            return NoContent(); 
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeletePost(int id)
        {
           var post = service.DeletePost(id);
            if(post == null)
            {
                return NotFound("Not found");
            }
            return NoContent();
        }
    }
    }
