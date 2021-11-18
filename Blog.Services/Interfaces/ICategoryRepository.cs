using Blog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Services.Interfaces
{
    public interface ICategoryRepository
    {
        //.net framekwork kısmında yaptıklarımızın post2u gibi düşünebilirsin burdakileri
        List<Category> GetCategories();

        Category GetCategory(int id);

        bool Add(Category entity);

        bool Edit(Category entity);

        bool Delete(int id);
    }


}
