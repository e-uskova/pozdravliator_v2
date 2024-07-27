using pozdravliator.Contracts.Person;
using pozdravliator.Contracts.Photo;
using pozdravliator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace pozdravliator.Infrastructure.DataAccess.Mapping
{
    public static class Mapper
    {
        public static PersonDto? ToPersonDto(Person? post)
        {
            if (post == null)
            {
                return null;
            }

            var result = new PersonDto()
            {
                Id = post.Id,
                Name = post.Name,
                Birthday = post.Birthday,
                NextBirthday = post.NextBirthday,
            };

            if (post.Photo != null)
            {
                result.Photo = ToPhotoInfoDto(post.Photo);
            }

            return result;
        }

        public static PhotoDto ToPhotoDto(Photo photo)
        {
            return new PhotoDto()
            {
                Id = photo.Id,
                Name = photo.Name,
                Content = photo.Content,
                ContentType = photo.ContentType,
            };
        }

        public static PhotoInfoDto ToPhotoInfoDto(Photo photo)
        {
            return new PhotoInfoDto()
            {
                Id = photo.Id,
                Name = photo.Name,
                ContentType = photo.ContentType,
                Length = photo.Length,
                Created = photo.Created,
                PersonId = photo.Person.Id
            };
        }
    }
}
