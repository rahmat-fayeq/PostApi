using AppApi.Data;
using AppApi.Model.Dto;
using AppApi.Model.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<PostDto> GetAllPosts()
        {
            var posts = db.Posts.OrderByDescending(x => x.Id).ToList();

            var postsDto = new List<PostDto>();
            foreach (var post in posts)
            {
                postsDto.Add(new PostDto()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,

                });
            }

            return postsDto;
        }

        public PostDto GetPost(int id)
        {
            var post = db.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return null;
            }

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
            };

            return postDto;
        }
        public ActionResult<PostDto> CreatePost(PostDto postDto)
        {
            var post = new Post
            {
                Id = postDto.Id,
                Title = postDto.Title,
                Body = postDto.Body,
            };

            db.Posts.Add(post);
            db.SaveChanges();

            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
            };
        }

        public PostDto UpdatePost(int id,PostDto postDto)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return null;
            }

            post.Title = postDto.Title;
            post.Body = postDto.Body;

            db.SaveChanges();

            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
            };

        }

        public ActionResult DeletePost(int id)
        {
            var post = db.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return null;
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            return new NoContentResult();
        }
    }
}
