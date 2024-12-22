using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.Dictionaries
{
    public class ConfigurationDictionaries
    {
        public Dictionary<byte, string> AuthenticationOperationDictionary { get; set; }
        public Dictionary<byte, string> LoginTypeDictionary { get; set; }
        public Dictionary<byte, string> OrganizationRelationshipDictionary { get; set; }
        public Dictionary<byte, string> ResidenceRelationshipDictionary { get; set; }
    }
}
