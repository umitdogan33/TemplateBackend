using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.logging.Serilog
{
    public interface ISeqLogger
    {
        void Configure(string fullName);
    }
}
