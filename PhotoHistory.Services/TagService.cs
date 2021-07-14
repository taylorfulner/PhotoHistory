using PhotoHistory.Data;
using PhotoHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Services
{
    public class TagService
    {
        private readonly Guid _adminId;

        public TagService() { }

        public TagService(Guid adminId)
        {
            _adminId = adminId;
        }

        public bool CreateTag(TagCreate model)
        {
            var entity = new Tag()
            {
                TagName = model.TagName,
                TagType = model.TagType
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Tags.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TagList> GetTags()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Tags
                    .Select(
                        e => new TagList
                        {
                            TagId = e.TagId,
                            TagName = e.TagName,
                            TagType = e.TagType
                        }
                    );
                return query.ToArray();
            }
        }

        public int GetTagId(string tagName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Tags
                    .Single(e => e.TagName == tagName);

                return entity.TagId;
            }
        }

        public IEnumerable<TagList> GetTagNames()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Tags
                    .Select(
                        e => new TagList
                        {
                            TagName = e.TagName
                        }
                    );
                return query.ToArray();
            }
        }


        public TagDetail GetTagById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Tags
                    .Single(e => e.TagId == id);
                return
                    new TagDetail
                    {
                        TagId = entity.TagId,
                        TagName = entity.TagName,
                        TagType = entity.TagType,
                        Photos = entity.Photos
                        .Select(x => new PhotoList()
                        {
                            PhotoName = x.Photo.PhotoName,
                            PhotoDate = x.Photo.PhotoDate
                        }
                        ).ToList()
                    };
            }
        }

        public bool UpdateTag(TagEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Tags
                    .Single(e => e.TagId == model.TagId);

                entity.TagName = model.TagName;
                entity.TagType = model.TagType;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTag(int tagId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Tags
                    .Single(e => e.TagId == tagId);

                ctx.Tags.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
