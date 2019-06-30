namespace BMWStore.Entities
{
    public class UserBoughtCar
    {
        public string CarId { get; set; }
        public BaseCar Car { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
