#include "DecipherCAFF.h"

void DecipherCAFF::DecryptFile(std::ifstream* infile, std::stringstream* decStream, unsigned char* aes_key, unsigned char* iv)
{
	AES_KEY dec_key;
	AES_set_decrypt_key(aes_key, 16, &dec_key);
	int length = infile->tellg();
	unsigned char* mDataBuffer= new unsigned char[length];
	unsigned char* dec_out = new unsigned char[length];
	infile->read((char*)(&mDataBuffer[0]), length);
	AES_cbc_encrypt(mDataBuffer, dec_out, length, &dec_key, iv, AES_DECRYPT);
	
	for (int i = 0; i < length; i++) {
		*decStream << dec_out[i];
	}

	delete[] mDataBuffer;
	delete[] dec_out;

}
