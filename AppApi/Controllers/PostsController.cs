using AppApi.Data;
using AppApi.Model.Dto;
using AppApi.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public PostsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult GetAllPosts()
        {
            var posts = db.Posts.OrderByDescending(x => x.Id).ToList();

            var postsDto = new List<PostDto>();
            foreach(var post in posts)
            {
                postsDto.Add(new PostDto()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,

                });
            }

            return Ok(postsDto);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetPost(int id) 
        {
            var post = db.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null) 
            {
                return NotFound("Post not found!");
            }

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
            };


            return Ok(postDto);
        }

        [HttpPost]
        public ActionResult CreatePost([FromBody] PostDto postDto)
        {
            var post = new Post
            {
                Id = postDto.Id,
                Title = postDto.Title,
                Body = postDto.Body,
            };

            db.Posts.Add(post);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new {Id = post.Id }, post);
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdatePost(int id,[FromBody]PostDto postDto)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

           post.Title = postDto.Title;
           post.Body = postDto.Body;

            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeletePost(int id)
        {
            var post = db.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null) 
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();
            return NoContent();
        }
    }
}
