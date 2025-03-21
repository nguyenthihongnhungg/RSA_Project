using System.Numerics;

namespace MyRSA
{
	internal class Program
	{
        public static byte[] random(int size)
        {
            // Tạo một mảng byte với kích thước size
            byte[] array = new byte[size];

            // Khởi tạo random
            Random random = new Random();

            // Tạo số ngẫu nhiên với tối thiểu giá trị lớn nhất của 1 số size byte
            random.NextBytes(array);

            // Đảm bảo số có giá trị tối thiểu, tức là không có byte bị "không đủ" giá trị
            // Đảm bảo byte đầu tiên không phải là 0 nếu size > 1
            if (size > 1 && array[0] == 0)
            {
                // Nếu byte đầu tiên là 0, ta thay đổi byte đầu tiên để đảm bảo giá trị tối thiểu.
                array[0] = (byte)1;
            }

            return array;
        }
        public static void Main(string[] args)
		{
            byte[] bytes = random(256);
            BigInteger x = new BigInteger(bytes);
            Console.WriteLine("Bit X: " + x.GetBitLength());
            byte[] bytes1 = random(256);
            BigInteger y = new BigInteger(bytes1);
            Console.WriteLine("Bit Y : " + y.GetBitLength());
            BigInteger z = x * y;
            Console.WriteLine("Bit Z: " + x.GetBitLength());

        }
		public static void testCert()
		{

        }
    }
}
