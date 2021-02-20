using Business.Abstract;
using Business.Constant;
using Core.Results;
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

        public IResult Add(Color entity)
        {
            
                _colorService.Add(entity);
                return new SuccessResult();
            
            
            

        }

        public IResult Delete(Color entity)
        {
            _colorService.Delete(entity);
            return new SuccessResult(Messages.DeleteMessage);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>();
        }

        public IResult Update(Color entity)
        {
            _colorService.Update(entity);
            return new SuccessResult(Messages.UpdateMessage);
        }
    }
}
