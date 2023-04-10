using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public static class ConfigurationKeysEnum
    {
#if DEBUG
        public static string DEVGamesDirectoryPath = "DevGamesDirPath";
#endif
        public static string GamesDirectoryPath = "GamesDirectoryPath";
    }
}
