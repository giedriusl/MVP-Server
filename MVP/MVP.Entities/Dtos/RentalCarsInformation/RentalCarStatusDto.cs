using MVP.Entities.Enums;

namespace MVP.Entities.Dtos.RentalCarsInformation
{
    public class RentalCarStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static RentalCarStatusDto ToDto(RentalCarStatus status)
        {
            return new RentalCarStatusDto
            {
                Id = (int)status,
                Name = status.ToString()
            };
        }
    }
}
