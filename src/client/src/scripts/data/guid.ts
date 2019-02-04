'use strict';

export class Guid extends String {
	static empty = '00000000-0000-0000-0000-000000000000';

	static newGuid = (): string => {
		return Guid.generateGuidPart(false) + Guid.generateGuidPart(true) + Guid.generateGuidPart(true) + Guid.generateGuidPart(false);
	};

	private static generateGuidPart = (isMiddlePart: boolean): string => {
		const part = (Math.random().toString(16) + '000000000').substr(2, 8);
		return isMiddlePart ? '-' + part.substr(0, 4) + '-' + part.substr(4, 4) : part;
	};
}