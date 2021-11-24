using Blog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Services.Interfaces
{
    public interface ITagRepository
    {
        List<Tag> GetTags();
        Tag GetTag(int id);
        bool Add(Tag entity);
        bool Edit(Tag entity);
        bool Delete(int id);
    }
}
