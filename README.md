# Solving The Orthographical Neighbour Problem
Adam Price
adamprice4@outlook.com

The following is a document detailing how I solved the ‘Orthographical Neighbor’ problem for the BluePrism technical test. I beginning with my first thoughts upon seeing the problem and progress to a detailed overview of the end system. I end by highlighting the weaknesses of my implementation. 

## Initial Thoughts

 - The dictionary will need to be cleaned of words longer and shorter than 4 letters. Words containing punctuation will also need to be removed. 

 - A tree can be formed from the input word (Will need a way to avoid cycles (taboo list?)) a breadth-first search would then be able to find the ‘closest’ solution. 

 - The search would need to examine the whole breadth of the tree after a solution is found in case multiple equal solutions exist. 

## The Solution

I will now highlight the steps taken by the solution to get a result. 

### The Dictionary
The dictionary file is found using the path provided by the user. It then undergoes a cleaning process in which words that are not 4 characters in length are removed. Words containing punctuation are also discarded and all words are converted to lowercase. 

The cleaning dramatically reduced the size of the dictionary (saving time when searching) and removes potential errors that would occur with words containing punctuation.

### Start And End Word Input
Validation is done on the inputs to prevent errors, such as, not allowing punctuation and forcing all inputs to lowercase. Also, the end word can only be accepted if it is in the dictionary.

### Finding Orthographical Neighbours

To find the orthographical neighbours of a word we loop through the whole dictionary comparing each word to the input word. Words that have 3 matching chars (and are not on the taboo list) will be kept to form child nodes. (O(n) complexity, but I don't think that that can be helped.)

### Breadth-First Search

Using a breadth-first search minimises the amount of time the ‘Find Orthographical Neighbors’ code will need to be run as the first solution to be found will also be the shortest (at least joined equally shortest).

The search creates a tree as it searches by creating a dictionary structure that links child nodes to its parent. When the end word is discovered the dictionary structure is used in a backtrace to produce the path taken from the start to the end word.

## Testing 
Minimal unit testing has been implemented. Far more could be done with this, but I have shown how unit testing could be applied to the solution.  


## Weaknesses And Closing Thoughts
- My implementation allows each node to have only one parent. This shouldn't prevent the system from finding a shortest solution, but it does limit it to only one. If the solution needed to be expanded to output all solutions of the shortest length, a proper tree would have to be formed. 

- The solution forms a tree as it searches for the solution. This is efficient in case of a single search, however, if we wanted to search for many orthographical neighbour 'chains'  it could be more efficient to create (and store) a network from the dictionary file. This network could then be queried to find chains using a breadth-first search. 

- The implementation is a little nebulous. The solving aspects of the program are given a separate class, but perhaps other aspects of the program should be separated into individual classes. At the same time, the solution is very small, and the main method of the program is easy to understand, so maybe a full OO implementation would be excessive. 

## Usage Example
This screenshot gives an example of how to use the program. 
https://imgur.com/7QpX9hX
