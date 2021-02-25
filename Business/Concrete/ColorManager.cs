using Business.Abstract;
using Business.Constant;
using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Business;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }


        public IResult Add(Color color)
        {
            var result = BusinessRules.Run(CheckIfNameExists(color.ColorName));
            if (result !=null)
            {
                return new ErrorResult();
            }
                _colorDal.Add(color);
                return new SuccessResult();
            
            
            

        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.DeleteMessage);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.UpdateMessage);
        }

        public IResult CheckIfNameExists(string colorName)
        {
            if (_colorDal.GetAll(c => c.ColorName.ToUpper() == colorName.ToUpper()).Count <=1)
            {
                return new ErrorResult(Messages.ColorNameExists);
            }

            return new SuccessResult();
        }
    }
}
