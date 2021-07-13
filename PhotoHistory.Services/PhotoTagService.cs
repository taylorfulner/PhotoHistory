using PhotoHistory.Data;
using PhotoHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Services
{
    public class PhotoTagService
    {
        public bool CreatePhotoTag(PhotoTagCreate model)
        {
            var entity = new PhotoTag()
                {
                    PhotoId = model.PhotoId,
                    TagId = model.TagId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.PhotoTags.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PhotoTagList> GetPhotoTags()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                        .PhotoTags
                        .Select(
                        e =>
                            new PhotoTagList
                            {
                                PhotoTagId = e.PhotoTagId,
                                PhotoId = e.PhotoId,
                                Photo = new PhotoList
                                {
                                    PhotoDate = e.Photo.PhotoDate,
                                    PhotoName = e.Photo.PhotoName
                                },
                                TagId = e.TagId,
                                Tag = new TagList
                                {
                                    TagName = e.Tag.TagName,
                                    TagType = e.Tag.TagType
                                }

                            });
                return query.ToArray();
            }
        }

        public PhotoTagDetail GetPhotoTagById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .PhotoTags
                    .Single(e => e.PhotoTagId == id);
                return
                    new PhotoTagDetail
                    {
                        PhotoTagId = entity.PhotoTagId,
                        PhotoId = entity.PhotoId,
                        Photo = new PhotoList
                        {
                            PhotoName = entity.Photo.PhotoName,
                            PhotoDate = entity.Photo.PhotoDate
                        },
                        TagId = entity.TagId,
                        Tag = new TagList
                        {
                            TagName = entity.Tag.TagName,
                            TagType = entity.Tag.TagType
                        }
                    };
            }
        }

        public bool UpdatePhotoTag(PhotoTagEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .PhotoTags
                    .Single(e => e.PhotoTagId == model.PhotoTagId);

                entity.PhotoTagId = model.PhotoTagId;
                entity.PhotoId = model.PhotoId;
                entity.TagId = model.TagId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePhotoTag(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .PhotoTags
                    .Single(e => e.PhotoTagId == id);
                ctx.PhotoTags.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
