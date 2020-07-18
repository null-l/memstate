using System.Collections.Generic;

namespace Memstate
{
    public class Serializers : Providers<ISerializer>
    {
        /// <summary>
        /// Take the first available serializer
        /// as determined by <see cref="AutoResolutionCandidates"/>
        /// </summary>
        public const string Auto = "Auto";

        /// <summary>
        /// Wire binary serializer
        /// </summary>
        public const string Wire = "Wire";

        /// <summary>
        /// NewtonSoft JSON serializer
        /// </summary>
        public const string NewtonsoftJson = "Newtonsoft.Json";

        public const string BinaryFormatter = "BinaryFormatter";

        private const string BinaryFormatterType = "Memstate.BinaryFormatterAdapter, Memstate.Core";
        private const string WireTypeName = "Memstate.Wire.WireSerializerAdapter, Memstate.Wire";
        private const string NewtonSoftJsonTypeName = "Memstate.JsonNet.JsonSerializerAdapter, Memstate.JsonNet";

        public Serializers()
        {
            Register(Auto, AutoResolve);
            Register(BinaryFormatter, () => InstanceFromTypeName(BinaryFormatterType));
            Register(Wire, () => InstanceFromTypeName(WireTypeName));
            Register(NewtonsoftJson, () => InstanceFromTypeName(NewtonSoftJsonTypeName));
        }

        protected override IEnumerable<string> AutoResolutionCandidates()
        {
            //If wire can be found, it is probable that the user has
            //explicitly added a reference with then intention of using it
            yield return Wire;
            
            yield return NewtonsoftJson;

            // Built into Memstate.Core so this is our fallback if no other serializer has
            // been referenced
            yield return BinaryFormatter;
        }
    }
}