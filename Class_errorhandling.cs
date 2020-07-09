using System;
using System.Diagnostics;

namespace roleplay
{
    public class Class_errorhandling
    {
        public void errorHandling(Exception exc)
        {
            try
            {
                var stackTrace = new StackTrace(exc, true);
                var frame = stackTrace.GetFrame(0);

                Console.WriteLine("Exception in file: {0}", frame.GetFileName());
                Console.WriteLine("Exception in method: {0}", frame.GetMethod());
                Console.WriteLine("Exception in line: {0}", frame.GetFileLineNumber());
                Console.WriteLine("Exception message: {0}", exc.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
