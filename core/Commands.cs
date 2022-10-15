using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splatoon3Tester.core
{
    enum Commands: int
    {
        COMMAND_STATUS = 0x01,

        COMMAND_POKE8 = 0x02,
        COMMAND_POKE16 = 0x03,
        COMMAND_POKE32 = 0x04,
        COMMAND_POKE64 = 0x05,

        COMMAND_READ = 0x06,
        COMMAND_WRITE = 0x07,
        COMMAND_CONTINUE = 0x08,
        COMMAND_PAUSE = 0x09,
        COMMAND_ATTACH = 0x0A,
        COMMAND_DETATCH = 0x0B,
        COMMAND_QUERY_MEMORY = 0x0C,
        COMMAND_QUERY_MEMORY_MULTI = 0x0D,
        COMMAND_CURRENT_PID = 0x0E,
        COMMAND_GET_ATTACHED_PID = 0x0F,
        COMMAND_GET_PIDS = 0x10,
        COMMAND_GET_TITLEID = 0x11,
        COMMAND_DISCONNECT = 0x12,
        COMMAND_READ_MULTI = 0x13,
        COMMAND_SET_BREAKPOINT = 0x14
    }
}
