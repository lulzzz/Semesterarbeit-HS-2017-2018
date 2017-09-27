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
package de.unibw.jarta.util;

import java.util.LinkedHashMap;
import java.util.Map;

/**
 * A cache holding a capped number of key-value pairs.
 * 
 * 
 * 
 * @author skip
 * @version $Revision: 1.0 $
 */
public class LruCache<K, V> extends LinkedHashMap<K, V> {

	/** The Constant serialVersionUID. */
	private static final long serialVersionUID = 1L;

	/** The max size. */
	private final int maxSize;



	/**
	 * Instantiates a new cache lru.
	 * 
	 * @param size
	 *            the maximal number of entries this cache can hold
	 */
	public LruCache(final int size) {
		super((int) (size * 1.35), 0.75F, true);

		maxSize = size;

	}



	/*
	 * (non-Javadoc)
	 * 
	 * @see java.util.LinkedHashMap#removeEldestEntry(java.util.Map.Entry)
	 */
	/**
	 * Method removeEldestEntry.
	 * 
	 * @param eldest
	 *            Map.Entry<K,V>
	 * @return boolean
	 */
	@Override
	protected boolean removeEldestEntry(final Map.Entry<K, V> eldest) {
		return size() > maxSize;
	}

}

