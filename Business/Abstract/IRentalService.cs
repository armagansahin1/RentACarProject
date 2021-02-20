using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IRentalService
    {
        
        IResult Update(Rental entity);
        IResult Delete(Rental entity);
        IDataResult<List<Rental>> GetAll();
        IResult Add(Rental rental);
        IDataResult<List<RentalDetailDto>> GetRentedCarDetail();
    }
}
