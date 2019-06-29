using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoinNCFiles_DTVM
{
    public interface IJoinNCfile
    {
        void Join(bool insertLine, string result, List<string> paths);
    }
}
