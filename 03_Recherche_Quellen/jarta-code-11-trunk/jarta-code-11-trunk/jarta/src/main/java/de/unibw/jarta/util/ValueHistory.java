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

/**
 * A container that stores a limited amount of values. Elements are stored in
 * order of their insertion. The element at position 0 is the newest entry while
 * the element at position size-1 is the oldest. The oldest value (according to
 * insertion time) is deleted whenever a new element is added to an full
 * ValueHistory.
 * 
 * @author tobias uhlig
 * 
 * @param <T>
 */
public interface ValueHistory<T> {

	/**
	 * Get an element at a given index. The index reflects the time when an
	 * element was added The element at position 0 is the newest entry while the
	 * element at position size-1 is the oldest.
	 * 
	 * @param index
	 * @return
	 */
	public T get(int index);



	/**
	 * Adds a new element to the History
	 * 
	 * @param element
	 */
	public void add(T element);



	/**
	 * Number of elements currently stored in the history
	 * 
	 * @return
	 */
	public int size();
	
	
}
