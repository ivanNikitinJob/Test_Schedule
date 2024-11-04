using Entities;

namespace Database
{
    public static class DataSeed
    {
        public static List<Interval> Intervals { get; }
        public static List<Group> Groups { get; }
        public static List<Address> Addresses { get; }

        static DataSeed()
        {
            Intervals = new List<Interval>();
            for (int i = 0; i < 24; i++)
            {
                Intervals.Add(
                    new Interval()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = new TimeOnly(i, 0),
                        EndTime = new TimeOnly((i + 1) % 24, 0)
                    });
            }

            Groups = new List<Group>();
            for (int i = 0; i < 5; i++)
            {
                Groups.Add(
                    new Group()
                    {
                        Id = Guid.NewGuid(),
                        Name = Enum.GetName((GroupsNames)i),
                        Number = i,
                        CreateDateTime = DateTime.UtcNow,
                    });
            }

            Addresses = new List<Address>();
            Addresses.Add(
                new Address()
                {
                    Street = "Ювілейна",
                    GroupId = Groups.ElementAt(0).Id,
                    Id = Guid.NewGuid(),
                });
            Addresses.Add(
                new Address()
                {
                    Street = "Маріупольська",
                    GroupId = Groups.ElementAt(1).Id,
                    Id = Guid.NewGuid(),
                });
            Addresses.Add(
                new Address()
                {
                    Street = "Свободи",
                    GroupId = Groups.ElementAt(2).Id,
                    Id = Guid.NewGuid(),
                });
            Addresses.Add(
                new Address()
                {
                    Street = "Миру",
                    GroupId = Groups.ElementAt(3).Id,
                    Id = Guid.NewGuid(),
                });
        }
    }

    public enum GroupsNames
    {
        First = 0,
        Second = 1,
        Third = 2,
        Forth = 3,
        Fifth = 4
    }
}
