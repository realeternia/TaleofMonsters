using System;
using System.Text;

namespace NarlonLib.Net
{
    public class NLRC4 : UXCryptoBase
    {
        private static NLRC4 RC4 = new NLRC4();

        public static Byte[] Encrypt(Byte[] data, String pass)
        {
            return RC4.EncryptEx(data, pass);
        }

        public static Byte[] Decrypt(Byte[] data, String pass)
        {
            return RC4.DecryptEx(data, pass);
        }

        public override Byte[] EncryptEx(Byte[] data, String pass)
        {
            if (data == null || pass == null) return null;
            Byte[] output = new Byte[data.Length];
            Int64 i = 0;
            Int64 j = 0;
            Byte[] mBox = GetKey(Encode.GetBytes(pass), 256);

            // ����
            for (Int64 offset = 0; offset < data.Length; offset++)
            {
                i = (i + 1) % mBox.Length;
                j = (j + mBox[i]) % mBox.Length;
                Byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;
                Byte a = data[offset];
                //Byte b = mBox[(mBox[i] + mBox[j] % mBox.Length) % mBox.Length];
                // mBox[j] һ���� mBox.Length С������Ҫ��ȡģ
                Byte b = mBox[(mBox[i] + mBox[j]) % mBox.Length];
                output[offset] = (Byte)((Int32)a ^ (Int32)b);
            }

            return output;
        }

        public override Byte[] DecryptEx(Byte[] data, String pass)
        {
            return EncryptEx(data, pass);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="pass">����</param>
        /// <param name="kLen">�����䳤��</param>
        /// <returns>���Һ������</returns>
        private static Byte[] GetKey(Byte[] pass, Int32 kLen)
        {
            Byte[] mBox = new Byte[kLen];

            for (Int64 i = 0; i < kLen; i++)
            {
                mBox[i] = (Byte)i;
            }
            Int64 j = 0;
            for (Int64 i = 0; i < kLen; i++)
            {
                j = (j + mBox[i] + pass[i % pass.Length]) % kLen;
                Byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;
            }
            return mBox;
        }
    }

    public class UXCryptoBase
    {
        /// <summary>
        /// ����ת�����������ֽ�����ַ���֮���ת����Ĭ��Ϊ��������
        /// </summary>
        public static Encoding Encode = Encoding.Default;

        public enum EncoderMode
        {
            Base64Encoder,
            HexEncoder
        };

        /// <summary>
        /// ������ģʽ���ַ�������
        /// </summary>
        /// <param name="data">Ҫ���ܵ�����</param>
        /// <param name="pass">����</param>
        /// <param name="em">����ģʽ</param>
        /// <returns>���ܺ󾭹�������ַ���</returns>
        public String Encrypt(String data, String pass, UXCryptoBase.EncoderMode em)
        {
            if (data == null || pass == null) return null;
            if (em == EncoderMode.Base64Encoder)
                return Convert.ToBase64String(EncryptEx(Encode.GetBytes(data), pass));
            else
                return ByteToHex(EncryptEx(Encode.GetBytes(data), pass));
        }

        /// <summary>
        /// ������ģʽ���ַ�������
        /// </summary>
        /// <param name="data">Ҫ���ܵ�����</param>
        /// <param name="pass">����</param>
        /// <param name="em">����ģʽ</param>
        /// <returns>����</returns>
        public String Decrypt(String data, String pass, UXCryptoBase.EncoderMode em)
        {
            if (data == null || pass == null) return null;
            if (em == EncoderMode.Base64Encoder)
                return Encode.GetString(DecryptEx(Convert.FromBase64String(data), pass));
            else
                return Encode.GetString(DecryptEx(HexToByte(data), pass));
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data">Ҫ���ܵ�����</param>
        /// <param name="pass">����</param>
        /// <returns>���ܺ󾭹�Ĭ�ϱ�����ַ���</returns>
        public String Encrypt(String data, String pass)
        {
            return Encrypt(data, pass, EncoderMode.Base64Encoder);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data">Ҫ���ܵľ������������</param>
        /// <param name="pass">����</param>
        /// <returns>����</returns>
        public String Decrypt(String data, String pass)
        {
            return Decrypt(data, pass, EncoderMode.Base64Encoder);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data">Ҫ���ܵ�����</param>
        /// <param name="pass">��Կ</param>
        /// <returns>����</returns>
        public virtual Byte[] EncryptEx(Byte[] data, String pass)
        {
            return null;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="data">Ҫ���ܵ�����</param>
        /// <param name="pass">����</param>
        /// <returns>����</returns>
        public virtual Byte[] DecryptEx(Byte[] data, String pass)
        {
            return null;
        }

        private static Byte[] HexToByte(String szHex)
        {
            // ����ʮ�����ƴ���һ���ֽ�
            Int32 iLen = szHex.Length;
            if (iLen <= 0 || 0 != iLen % 2)
            {
                return null;
            }
            Int32 dwCount = iLen / 2;
            UInt32 tmp1, tmp2;
            Byte[] pbBuffer = new Byte[dwCount];
            for (Int32 i = 0; i < dwCount; i++)
            {
                tmp1 = (UInt32)szHex[i * 2] - (((UInt32)szHex[i * 2] >= (UInt32)'A') ? (UInt32)'A' - 10 : (UInt32)'0');
                if (tmp1 >= 16) return null;
                tmp2 = (UInt32)szHex[i * 2 + 1] -
                       (((UInt32)szHex[i * 2 + 1] >= (UInt32)'A') ? (UInt32)'A' - 10 : (UInt32)'0');
                if (tmp2 >= 16) return null;
                pbBuffer[i] = (Byte)(tmp1 * 16 + tmp2);
            }
            return pbBuffer;
        }

        private static String ByteToHex(Byte[] vByte)
        {
            if (vByte == null || vByte.Length < 1) return null;
            StringBuilder sb = new StringBuilder(vByte.Length * 2);
            for (int i = 0; i < vByte.Length; i++)
            {
                if ((UInt32)vByte[i] < 0) return null;
                UInt32 k = (UInt32)vByte[i] / 16;
                sb.Append((Char)(k + ((k > 9) ? 'A' - 10 : '0')));
                k = (UInt32)vByte[i] % 16;
                sb.Append((Char)(k + ((k > 9) ? 'A' - 10 : '0')));
            }
            return sb.ToString();
        }
    }
}