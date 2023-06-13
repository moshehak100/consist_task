using consist.DataTypes;
using consist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consist.Services
{

    public class FamilyTreeService : IFamilyTreeService
    {
        private readonly IJsonConvertor _jsonConvertor;
        public FamilyTreeService(IJsonConvertor jsonConvertor)
        {
            _jsonConvertor = jsonConvertor;
        }

        public Task<List<FamilyPerson>> ConvertPersonsListToFamilyTree(string familyList)
        {
            List<FamilyPerson> familyPersons = _jsonConvertor.JsonListToObjects(familyList);

            setChildsForFamilyPersons(ref familyPersons);

            return Task.FromResult(getRootAncestors(familyPersons));
        }

        private List<FamilyPerson> getRootAncestors(List<FamilyPerson> familyPersons)
        {
            return familyPersons.Where(x => x.parent == null).ToList();
        }
        private void setChildsForFamilyPersons(ref List<FamilyPerson> familyPersons)
        {
            Dictionary<int, FamilyPerson> familyTree = familyPersons.ToDictionary(x => x.id, x => x);

            foreach (FamilyPerson person in familyPersons)
            {
                if (person.parent != null)
                {
                    familyTree[person.parent.Value].childs.Add(person);
                }
            }
        }

    }
}
