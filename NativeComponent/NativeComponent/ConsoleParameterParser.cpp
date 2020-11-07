#include "ConsoleParameterParser.h"
#include <iostream>
using namespace ConsoleParameterParserNamespace;

void ConsoleParameterParser::handleConsoleParameters(int argc, char* argv[]) {
	for (int i = 1; i < argc; i += 2) {
		if ((i + 1) < argc) {
			setParameter(string(argv[i]), string(argv[i + 1]));
		}
	}

	std::cout << "Filename: " + filename + "\nMode: " + mode + "\nHashcode: " + hashcode + "\n";
}

void ConsoleParameterParser::setParameter(string param, string value) {
	if (param.rfind("-filename", 0) == 0) {
		filename = value;
	}

	if (param.rfind("-hashcode", 0) == 0) {
		hashcode = value;
	}

	if (param.rfind("-mode", 0) == 0) {
		mode = value;
	}
}