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
        public static ThemebookType Mythos = new ThemebookType(1, "Mythos");
        public static ThemebookType Logos = new ThemebookType(2, "Logos");
        public static ThemebookType Mist = new ThemebookType(3, "Mist");
        public static ThemebookType Crew = new ThemebookType(4, "Crew");
        public static ThemebookType Extra = new ThemebookType(5, "Extra");
        #endregion

        public static List<ThemebookType> All = new List<ThemebookType>
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
                                                                ?? throw new DomainException($"Themebook type with id value {id} not foud");
        public static ThemebookType GetThemebookByName(string name) => All.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()))
                                                                    ?? throw new DomainException($"Themebook type with name value {name} not foud");

    }
}
