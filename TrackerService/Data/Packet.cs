using System;

namespace TrackerService.Data;

public class Packet {
	// fields ----------------------------------------------------------------------------------- //
	private readonly byte _messageType;
	private readonly byte _packetIdentifier;
	private readonly byte[] _payloadLength;
	private readonly byte[] _payload;
	private readonly byte[] _crc;

	// constructors ----------------------------------------------------------------------------- //
	public Packet(byte[] data) {
		int index = 0;

		this._messageType = data[index++];
		this._packetIdentifier = data[index++];

		this._payloadLength = data[index..(index + 2)];

		index += 2;

		this._payload = data[index..(index + this.PayloadLength)];

		index += this.PayloadLength;

		this._crc = data[index..(index + 2)];
	}



	// properties ------------------------------------------------------------------------------- //
	public int CRC => this._crc[0] << 8 | this._crc[1];

	public int PacketIdentifier => this._packetIdentifier;

	public byte[] Payload => this._payload;

	public int PayloadLength => this._payloadLength[0] << 8 | this._payloadLength[1];

	public int MessageType => this._messageType;

	// methods ---------------------------------------------------------------------------------- //
	public override string ToString() {
		return (
			$"Message Type: {this._messageType}\n" +
			$"Packet Identifier: {this._packetIdentifier}\n" +
			$"Payload Length: {this._payload.Length}\n" +
			$"Payload: {BitConverter.ToString(this._payload)}\n" +
			$"CRC: {this.CRC}\n" +
			$"Valid: {this.IsValid()}"
		);
	}

	public bool IsValid() {
		return CRC16.Calculate(
			[this._messageType, this._packetIdentifier, .. this._payloadLength, .. this._payload]
		) == this.CRC;
	}

	public byte[] Raw() {
		byte[] raw = new byte[6 + this.PayloadLength];

		raw[0] = this._messageType;
		raw[1] = this._packetIdentifier;
		raw[2] = this._payloadLength[0];
		raw[3] = this._payloadLength[1];

		for (int i = 0; i < this.PayloadLength; i++) {
			raw[i + 4] = this._payload[i];
		}

		raw[^2] = this._crc[0];
		raw[^1] = this._crc[1];

		return raw;
	}
}
