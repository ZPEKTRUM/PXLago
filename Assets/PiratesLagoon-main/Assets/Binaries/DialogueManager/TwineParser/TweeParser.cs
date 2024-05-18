using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TwineParser
{
    public class TweeParser : MonoBehaviour {
    
    // The passage class, which is just title, tags, and body
        [System.Serializable]
        public class TweePassage {
            public string _title = null;
            public string[] _tags;
            public string _body = null;
            public string _character = null;
            public List<PassageLink> _links;
            public bool _isEnd = false;
        }

        [System.Serializable]
        public class PassageLink {
            public string _passageName;
            public string _linkText;
        }
        
        // The TextAsset resource we'll be parsing
        public TextAsset _tweeSourceAsset;
        
        // Dictionary to hold the passages, keyed by their titles
        public Dictionary<string, TweePassage> _passages =
            new Dictionary<string, TweePassage>();

        [HideInInspector] public string _firstPassage = null;
        
        // I found it useful during development to have an inspectable
        // array of the titles of the loaded passages.
        [HideInInspector] public string[] titles = new string[0];
        
        // The one big string that will hold the twee source file
        protected string tweeSource;
        
        // Where the magic happens
        public void Parse() 
        {
            // Load the twee source from the asset
            tweeSource = _tweeSourceAsset.text;
            
            // Populate the reference array
            titles = new string[_passages.Count];
            _passages.Keys.CopyTo(titles, 0);
        
            // A reference to the passage we're currently building from the source
            TweePassage currentPassage = null;
            
            // Buffer to hold the content of the current passage while we build it
            StringBuilder buffer = new StringBuilder();
            
            // Array that will hold all of the individual lines in the twee source
            string[] lines; 
            
            // Utility array used in various instances where a string needs to be split up
            string[] chunks;
            
            // Split the twee source into lines so we can make sense of it while parsing
            lines = tweeSource.Split(new string[] {"\n"}, System.StringSplitOptions.None);
            
            // Just iterating through the whole file here
            for (long i = 0; i < lines.LongLength; i++) {
                
                // If a line begins with "::" that means a new passage has started
                if (lines[i].StartsWith("::")) 
                {
                    
                    // If we were already building a passage, that one is done.
                    // Wrap it up and add it to the dictionary of passages. 
                    if (currentPassage != null) {
                        currentPassage._body = buffer.ToString();
                        _passages.Add(currentPassage._title, currentPassage);                 
                        buffer = new StringBuilder();
                    }
                    
                    /* I know, I know, a magic number and chained function calls and it's
                    * ugly, but it's not that complicated. A new passage in a twee file
                    * starts with a line like this:
                    *
                    * :: The Passage Begins Here [someTag anotherTag heyThere]
                    *               
                    * What's happening here is when a new passage starts, we ignore the
                    * :: prefix, strip off the ] at the end of the tags, and split the
                    * line on [ into two strings, one of which will be the passage title
                    * while the other has all of the passage's tags, if any are found.
                    */
                    chunks = lines[i].Substring(2).Replace ("]", "").Split ('[');
                    
                    // We should always have at least a passage title, so we can
                    // start a new passage here with that title.
                    currentPassage = new TweePassage();
                    currentPassage._title = chunks[0].Trim();
                    currentPassage._links = new List<PassageLink>();
                    
                    // If there was anything after the [, the passage has tags, so just
                    // split them up and attach them to the passage.
                    if (chunks.Length > 1) {
                        currentPassage._tags = chunks[1].Trim().Split(' ');  
                        foreach (string s in currentPassage._tags)
                        {
                            if (s.Contains("e:Start"))
                            {
                                _firstPassage = currentPassage._title;
                            }
                            else if(s.Contains("c:"))
                            {
                                currentPassage._character = s.Substring(2);
                            }
                            else if (s.Contains("e:End"))
                            {
                                currentPassage._isEnd = true;
                            }
                        }
                    }

                } else if (lines[i].StartsWith("[["))
                {
                    PassageLink link = new PassageLink();
                    
                    // '|' is the separator between the response text and link, it not mandatory
                    int pipeIdx = lines[i].IndexOf('|');
                    if (pipeIdx == -1){
                        link._linkText = link._passageName = lines[i].Substring(2).Replace ("]]", "");
                    }
                    else {
                        link._linkText = lines[i].Substring(2, pipeIdx-2);
                        link._passageName = lines[i].Substring(pipeIdx+1).Replace ("]]", "").Trim();
                    }
                    currentPassage._links.Add(link);
                
                } else if (currentPassage != null) {
                
                    // If we didn't start a new passage, we're still in the previous one,
                    // so just append this line to the current passage's buffer.
                    buffer.AppendLine(lines[i]);    
                }
            }
            
            // When we hit the end of the file, we should still have the last passage in
            // the file in the buffer. Wrap it up and end it as well.
            if (currentPassage != null) {           
                currentPassage._body = buffer.ToString();
                _passages.Add(currentPassage._title, currentPassage);
            }
        }
    }
}