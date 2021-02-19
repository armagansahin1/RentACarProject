using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorService _colorService;

        public ColorManager(IColorService colorService)
        {
            _colorService = colorService;
        }

        public void Add(Color entity)
        {
            _colorService.Add(entity);
        }

        public void Delete(Color entity)
        {
            _colorService.Add(entity);
        }

        public List<Color> GetAll()
        {
            return _colorService.GetAll();
        }

        public void Update(Color entity)
        {
            _colorService.Add(entity);
        }
    }
}
