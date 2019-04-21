using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.mxProject.Helpers.Common
{
    [MessagePack.MessagePackObject]
    public sealed class TestResponse
    {
        [MessagePack.Key(0)]
        public int IntValue { get; set; }

        [MessagePack.Key(1)]
        public DateTime DateTimeValue { get; set; }
    }

    [MessagePack.MessagePackObject]
    public struct TestResponseStruct
    {
        [MessagePack.Key(0)]
        public int IntValue { get; set; }

        [MessagePack.Key(1)]
        public DateTime DateTimeValue { get; set; }
    }
}
