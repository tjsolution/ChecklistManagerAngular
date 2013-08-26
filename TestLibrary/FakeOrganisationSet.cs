using ChecklistManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace TestLibrary
{
    public class FakeOrganisationSet : FakeDbSet<Organisation>
    {
        public override Organisation Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.Name == keyValues.Single() as string);
        }
    }
}
