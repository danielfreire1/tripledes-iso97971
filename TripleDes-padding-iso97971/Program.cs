using System;

namespace TripleDes_padding_iso97971
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CryptyUtil.Decrypt3DSISO97971("key", "data"));
        }
    }
}
