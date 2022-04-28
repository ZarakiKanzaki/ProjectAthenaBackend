using AthenaBackend.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Domain.WellKnownInstances
{
    public class ThemebookType
    {
        public ThemebookType(short id, string name)
        {
            Id = id;
            Name = name;
        }

        public short Id { get; set; }
        public string Name { get; set; }
    }

    public static class ThemebookTypes
    {
        #region Themebooks
        public readonly static ThemebookType Mythos = new(1, "Mythos");
        public readonly static ThemebookType Logos = new(2, "Logos");
        public readonly static ThemebookType Mist = new(3, "Mist");
        public readonly static ThemebookType Crew = new(4, "Crew");
        public readonly static ThemebookType Extra = new(5, "Extra");
        #endregion

        public readonly static List<ThemebookType> All = new()
        {
            Mythos,
            Logos,
            Mist,
            Crew,
            Extra,
        };

        public static ThemebookType FindThemebookByKey(short id) => All.FirstOrDefault(a => a.Id == id);
        public static ThemebookType FindThemebookByName(string name) => All.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
        public static ThemebookType GetThemebookByKey(short id) => All.FirstOrDefault(a => a.Id == id)
                                                                ?? throw new CannotFindEntityDomainException(nameof(ThemebookType), nameof(id), id);
        public static ThemebookType GetThemebookByName(string name) => All.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()))
                                                                ?? throw new CannotFindEntityDomainException(nameof(ThemebookType), nameof(name), name);

    }
}
