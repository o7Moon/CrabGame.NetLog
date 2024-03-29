using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Packet : IDisposable
{
	public static explicit operator Il2CppPacket(Packet p){
		return new Il2CppPacket(p.buffer.ToArray());
	}

	public List<byte> buffer;

	public byte[] readableBuffer;

	public int readPos;

	public bool disposed;

	public Packet()
	{
		this.buffer = new List<byte>();
		this.readPos = 0;
	}

	public Packet(int _id)
	{
		this.buffer = new List<byte>();
		this.readPos = 0;
		this.Write(_id);
	}

	public Packet(byte[] _data)
	{
		this.buffer = new List<byte>();
		this.readPos = 0;
		this.SetBytes(_data);
	}

	public void SetBytes(byte[] _data)
	{
		this.Write(_data);
		this.readableBuffer = this.buffer.ToArray();
	}

	public void WriteLength()
	{
		this.buffer.InsertRange(0, BitConverter.GetBytes(this.buffer.Count));
	}

	public void InsertInt(int _value)
	{
		this.buffer.InsertRange(0, BitConverter.GetBytes(_value));
	}

	public byte[] ToArray()
	{
		this.readableBuffer = this.buffer.ToArray();
		return this.readableBuffer;
	}

	public int Length()
	{
		return this.buffer.Count;
	}

	public int UnreadLength()
	{
		return this.Length() - this.readPos;
	}

	public void Reset(bool _shouldReset = true)
	{
		if (_shouldReset)
		{
			this.buffer.Clear();
			this.readableBuffer = null;
			this.readPos = 0;
			return;
		}
		this.readPos -= 4;
	}

	public void Write(byte _value)
	{
		this.buffer.Add(_value);
	}

	public void Write(byte[] _value)
	{
		this.buffer.AddRange(_value);
	}

	public void Write(short _value)
	{
		this.buffer.AddRange(BitConverter.GetBytes(_value));
	}

	public void Write(int _value)
	{
		this.buffer.AddRange(BitConverter.GetBytes(_value));
	}

	public void Write(long _value)
	{
		this.buffer.AddRange(BitConverter.GetBytes(_value));
	}

	public void Write(ulong _value)
	{
		this.buffer.AddRange(BitConverter.GetBytes(_value));
	}

	public void Write(float _value)
	{
		this.buffer.AddRange(BitConverter.GetBytes(_value));
	}

	public void Write(bool _value)
	{
		this.buffer.AddRange(BitConverter.GetBytes(_value));
	}

	public void Write(string _value)
	{
		this.Write(_value.Length);
		this.buffer.AddRange(Encoding.ASCII.GetBytes(_value));
	}

	public void Write(Vector3 _value)
	{
		this.Write(_value.x);
		this.Write(_value.y);
		this.Write(_value.z);
	}

	public void Write(Quaternion _value)
	{
		this.Write(_value.x);
		this.Write(_value.y);
		this.Write(_value.z);
		this.Write(_value.w);
	}

	public byte ReadByte(bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			byte arg_31_0 = this.readableBuffer[this.readPos];
			if (_moveReadPos)
			{
				this.readPos++;
			}
			return arg_31_0;
		}
		throw new Exception("Could not read value of type 'byte'!");
	}

	public byte[] ReadBytes(int _length, bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			byte[] arg_3B_0 = this.buffer.GetRange(this.readPos, _length).ToArray();
			if (_moveReadPos)
			{
				this.readPos += _length;
			}
			return arg_3B_0;
		}
		throw new Exception("Could not read value of type 'byte[]'!");
	}

	public byte[] CloneBytes()
	{
		return this.buffer.ToArray();
	}

	public short ReadShort(bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			short arg_35_0 = BitConverter.ToInt16(this.readableBuffer, this.readPos);
			if (_moveReadPos)
			{
				this.readPos += 2;
			}
			return arg_35_0;
		}
		throw new Exception("Could not read value of type 'short'!");
	}

	public int ReadInt(bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			int arg_35_0 = BitConverter.ToInt32(this.readableBuffer, this.readPos);
			if (_moveReadPos)
			{
				this.readPos += 4;
			}
			return arg_35_0;
		}
		throw new Exception("Could not read value of type 'int'!");
	}

	public long ReadLong(bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			long arg_35_0 = BitConverter.ToInt64(this.readableBuffer, this.readPos);
			if (_moveReadPos)
			{
				this.readPos += 8;
			}
			return arg_35_0;
		}
		throw new Exception("Could not read value of type 'long'!");
	}

	public ulong ReadUlong(bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			ulong arg_35_0 = BitConverter.ToUInt64(this.readableBuffer, this.readPos);
			if (_moveReadPos)
			{
				this.readPos += 8;
			}
			return arg_35_0;
		}
		throw new Exception("Could not read value of type 'ulong'!");
	}

	public float ReadFloat(bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			float arg_35_0 = BitConverter.ToSingle(this.readableBuffer, this.readPos);
			if (_moveReadPos)
			{
				this.readPos += 4;
			}
			return arg_35_0;
		}
		throw new Exception("Could not read value of type 'float'!");
	}

	public bool ReadBool(bool _moveReadPos = true)
	{
		if (this.buffer.Count > this.readPos)
		{
			bool arg_35_0 = BitConverter.ToBoolean(this.readableBuffer, this.readPos);
			if (_moveReadPos)
			{
				this.readPos++;
			}
			return arg_35_0;
		}
		throw new Exception("Could not read value of type 'bool'!");
	}

	public string ReadString(bool _moveReadPos = true)
	{
		string result;
		try
		{
			int num = this.ReadInt(true);
			string @string = Encoding.ASCII.GetString(this.readableBuffer, this.readPos, num);
			if (_moveReadPos && @string.Length > 0)
			{
				this.readPos += num;
			}
			result = @string;
		}
		catch
		{
			throw new Exception("Could not read value of type 'string'!");
		}
		return result;
	}

	public Vector3 ReadVector3(bool moveReadPos = true)
	{
		return new Vector3(this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos));
	}

	public Quaternion ReadQuaternion(bool moveReadPos = true)
	{
		return new Quaternion(this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos));
	}

	protected virtual void Dispose(bool _disposing)
	{
		if (!this.disposed)
		{
			if (_disposing)
			{
				this.buffer = null;
				this.readableBuffer = null;
				this.readPos = 0;
			}
			this.disposed = true;
		}
	}

	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}
}