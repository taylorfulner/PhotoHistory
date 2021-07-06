using PhotoHistory.Data;
using PhotoHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Services
{
    public class PhotoService
    {
        private readonly Guid _adminId;

        public PhotoService(Guid adminId)
        {
            _adminId = adminId;
        }

        public bool CreatePhoto(PhotoCreate model)
        {
            var entity = new Photo()
            {
                AdminId = _adminId,
                PhotoName = model.PhotoName,
                PhotoDesc = model.PhotoDesc,
                PhotoDate = model.PhotoDate,
                PhotoStorageLocation = model.PhotoStorageLocation,
                PhotoUploadDate = DateTimeOffset.UtcNow
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Photos.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PhotoList> GetPhotos()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Photos
                    .Select(
                        e => new PhotoList
                        {
                            PhotoId = e.PhotoId,
                            PhotoName = e.PhotoName,
                            PhotoDate = e.PhotoDate,
                            PhotoStorageLocation = e.PhotoStorageLocation
                        }
                    );
                return query.ToArray();
            }
        }

        public PhotoDetail GetPhotoById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Photos
                    .Single(e => e.PhotoId == id);
                return
                    new PhotoDetail
                    {
                        PhotoId = entity.PhotoId,
                        PhotoName = entity.PhotoName,
                        PhotoDesc = entity.PhotoDesc,
                        PhotoDate = entity.PhotoDate,
                        PhotoUploadDate = entity.PhotoUploadDate,
                        AdminId = _adminId,
                        PhotoStorageLocation = entity.PhotoStorageLocation,
                        Tags = entity.Tags
                        .Select(x => new TagList()
                        {
                            TagName = x.Tag.TagName,
                            TagType = x.Tag.TagType,
                        }
                        ).ToList()
                    };
            }
        }

        public bool UpdatePhoto(PhotoEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Photos
                    .Single(e => e.PhotoId == model.PhotoId);

                entity.PhotoName = model.PhotoName;
                entity.PhotoDesc = model.PhotoDesc;
                entity.PhotoDate = model.PhotoDate;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePhoto(int photoId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Photos
                    .Single(e => e.PhotoId == photoId);

                ctx.Photos.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
