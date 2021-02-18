using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorManager
    {
        IColorManager _colorManager;

        public ColorManager(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        public void Add(Color entity)
        {
               _colorManager.Add(entity);
        }

        public void Delete(Color entity)
        {
            _colorManager.Delete(entity);
        }

        public List<Color> GetAll()
        {
            return _colorManager.GetAll();
        }

        public void Update(Color entity)
        {
            _colorManager.Update(entity);
        }
    }
}
