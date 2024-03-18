namespace TrackerService.Data;

public static class CRC16 {
	// methods ---------------------------------------------------------------------------------- //
	public static ushort Calculate(byte[] data) {
		// ushort crc = _initialValue;

		// for (int i = 0; i < data.Length; i++) {
		// 	crc = (ushort) (_table[(ushort) ((data[i] ^ (crc >> 8)) & 0xFF)] ^ (crc << 8));
		// }

		// return crc;

		ushort crc = 0xFFFF;

		for (int i = 0; i < data.Length; i++) {
			crc ^= (ushort) (data[i] << 8);

			for (int j = 0; j < 8; j++) {
				if ((crc & 0x8000) > 0) {
					crc = (ushort) ((crc << 1) ^ 0x1021);
				} else {
					crc <<= 1;
				}
			}
		}

		return crc;
	}
}
