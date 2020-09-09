Solving The Orthographical Neighbor Problem
Adam Price - adamprice4@outlook.com

The following is a document detailing how I solved the ‘Orthographical Neighbor’ problem for the BluePrism Technical test. Beginning with my first thoughts upon seeing the problem and progressing to more detailed ideas and then an overview of the end system. 

Initial thoughts:  

The dictionary will need to be cleaned of words longer and shorter than 4 letters. Words containing punctuation will also need to be removed. 

A tree can be formed from the input word (Will need a way to avoid cycles (taboo list?)) a breadth-first search would then be able to find the ‘closest’ solution. 

The search would need to examine the whole breadth of the tree after a solution is found in case multiple equal solutions exist. 

Find Orthographical Neighbors:

Loop the whole dictionary comparing each word to the input word, words that have 3 matching chars (and are not on the taboo list) will be kept to form child nodes. (O(n) complexity, but I don't think that that can be helped.)


Breadth-First Search:

Using breadth-frist will minimise the amount of time the ‘Find Othographical Neighbors’ code will need to be run as the first solution to be found will also be the shortest (at least joined equally shortest). 


Weaknesses: 

My implementation allows each node to have only one parent. This shouldn't prevent the system from finding a shortest solution, but it does limited it to only one. If the solution needed to expanded to output all solutions of the shortest length, a proper tree would have to be formed. 
