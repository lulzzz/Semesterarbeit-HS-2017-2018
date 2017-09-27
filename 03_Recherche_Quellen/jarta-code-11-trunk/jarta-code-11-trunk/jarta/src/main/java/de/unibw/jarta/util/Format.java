/*******************************************************************************
 * Copyright 2013 by Tobias Uhlig
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 ******************************************************************************/
/*
 * sage - a framework to evaluate Scheduling Heuristics (SH -> sage) for typical
 * scheduling problems in semiconductor manufacturing. It contains a simple
 * discrete event simulator which uses approximation methods to simulate
 * schedules. Schedules can be evaluate according to different objective values.
 * 
 * by Tobias Uhlig - tobias.uhlig@unibw.de
 */

package de.unibw.jarta.util;

import java.text.DecimalFormat;
import java.text.SimpleDateFormat;

/**
 * This Class provides some Format Helpers and platform dependent symbols.
 * 
 * @author skip
 * @version $Revision: 1.0 $
 */
public final class Format {

	/** Format to limit the digits of double to three. */
	public static final DecimalFormat THREE_DIGITS = new DecimalFormat("#0.000");

	/** Format to limit the digits of double to two. */
	public static final DecimalFormat TWO_DIGITS = new DecimalFormat("#0.00");

	/** Format to limit the digits of double to one. */
	public static final DecimalFormat ONE_DIGIT = new DecimalFormat("#0.0");

	/** A short date representation */
	public static final SimpleDateFormat SIMPLE_DATE = new SimpleDateFormat("yyMMdd-HHmmss");

	/** The Constant platform independent NEWLINE. */
	public static final String NEWLINE = System.getProperty("line.separator");

	/** The Constant platform independent TAB. */
	public static final String TAB = "\t";



	private Format() {
		// no instantiation
	}

}
