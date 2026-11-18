using System;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class ShopGeneralHourBuilder
    {

        private readonly ShopGeneralHour _entity = new ShopGeneralHour();

        public ShopGeneralHourBuilder()
        {
            _entity.DateOfWeek = "Monday";
            _entity.StartingTime = DateTime.Now;
            _entity.EndingTime = DateTime.Now;
            _entity.Created = DateTime.Now;
        }



        public ShopGeneralHour BuildMonday(DateTime? startTime=null,DateTime? stopTime=null)
        {


            _entity.DateOfWeek = DayOfWeek.Monday.ToString();
            _entity.StartingTime=startTime??new DateTime(1900,01,01,17,00,00);
            _entity.EndingTime=stopTime??new DateTime(1900,01,01,03,00,00);

            return _entity;

        }


        public ShopGeneralHour BuildTuesday(DateTime? startTime = null, DateTime? stopTime = null)
        {


            _entity.DateOfWeek = DayOfWeek.Tuesday.ToString();
            _entity.StartingTime = startTime ?? new DateTime(1900, 01, 01, 17, 00, 00);
            _entity.EndingTime = stopTime ?? new DateTime(1900, 01, 01, 03, 00, 00);

            return _entity;

        }

        public ShopGeneralHour BuildWednesday(DateTime? startTime = null, DateTime? stopTime = null)
        {


            _entity.DateOfWeek = DayOfWeek.Wednesday.ToString();
            _entity.StartingTime = startTime ?? new DateTime(1900, 01, 01, 17, 00, 00);
            _entity.EndingTime = stopTime ?? new DateTime(1900, 01, 01, 03, 00, 00);

            return _entity;

        }


        public ShopGeneralHour BuildThursday(DateTime? startTime = null, DateTime? stopTime = null)
        {


            _entity.DateOfWeek = DayOfWeek.Thursday.ToString();
            _entity.StartingTime = startTime ?? new DateTime(1900, 01, 01, 17, 00, 00);
            _entity.EndingTime = stopTime ?? new DateTime(1900, 01, 01, 03, 00, 00);

            return _entity;

        }


        public ShopGeneralHour BuildFriday(DateTime? startTime = null, DateTime? stopTime = null)
        {


            _entity.DateOfWeek = DayOfWeek.Friday.ToString();
            _entity.StartingTime = startTime ?? new DateTime(1900, 01, 01, 17, 00, 00);
            _entity.EndingTime = stopTime ?? new DateTime(1900, 01, 01, 03, 00, 00);

            return _entity;

        }
        public ShopGeneralHour BuildSaturday(DateTime? startTime = null, DateTime? stopTime = null)
        {


            _entity.DateOfWeek = DayOfWeek.Saturday.ToString();
            _entity.StartingTime = startTime ?? new DateTime(1900, 01, 01, 17, 00, 00);
            _entity.EndingTime = stopTime ?? new DateTime(1900, 01, 01, 03, 00, 00);

            return _entity;

        }

        public ShopGeneralHour BuildSunday(DateTime? startTime = null, DateTime? stopTime = null)
        {


            _entity.DateOfWeek = DayOfWeek.Sunday.ToString();
            _entity.StartingTime = startTime ?? new DateTime(1900, 01, 01, 17, 00, 00);
            _entity.EndingTime = stopTime ?? new DateTime(1900, 01, 01, 03, 00, 00);

            return _entity;

        }



        public ShopGeneralHour Build()
        {
            return _entity;
        }
    }
}