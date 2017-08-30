#MIT License

#Copyright (c) 2017 Vijayshinva Karnure

#Permission is hereby granted, free of charge, to any person obtaining a copy
#of this software and associated documentation files (the "Software"), to deal
#in the Software without restriction, including without limitation the rights
#to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
#copies of the Software, and to permit persons to whom the Software is
#furnished to do so, subject to the following conditions:

#The above copyright notice and this permission notice shall be included in all
#copies or substantial portions of the Software.

#THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
#IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
#FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
#AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
#LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
#OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
#SOFTWARE.
from bs4 import BeautifulSoup
import requests
import sys
import os
from collections import Counter

def generate_csharp_enum(colors):
    with open('NamedColors.cs', 'w') as f:
        f.write('namespace BeautifulColors')
        f.write('{'+ os.linesep)
        f.write('public enum NamedColors')
        f.write('{'+ os.linesep)
        for name, hexcode in colors:
            f.write(name+' = 0x'+hexcode[1:]+','+os.linesep)
        f.write('}'+ os.linesep)
        f.write('}'+ os.linesep)

def deduplicate_list(data):
    seen = set()
    out = [x for x in data if x[0] not in seen and not seen.add(x[0])]
    return out

def extract_page(url):
    page = requests.get(url)
    if page.status_code == 200:
        return page.content
    return None

def extract_colors(content):
    colors = []
    soup = BeautifulSoup(content, 'html.parser')
    for tr in soup.find_all('tr'):
        try:
            name = camel_case(tr.find('a').text)
            color = tr.find('td').renderContents()
            color = color.decode('UTF-8')
            if color.startswith('#'):
                print(name, ' = 0x'+color[1:])
                colors.append((name, color))    
        except :
            pass
    return colors

def camel_case(data):
    return ''.join(x for x in data.title() if x.isalnum())

def main():
    colors = []
    page = extract_page('https://en.wikipedia.org/wiki/List_of_colors:_A%E2%80%93F')
    if page != None:
        colors.extend(extract_colors(page))
    page = extract_page('https://en.wikipedia.org/wiki/List_of_colors:_G%E2%80%93M')
    if page != None:
        colors.extend(extract_colors(page))
    page = extract_page('https://en.wikipedia.org/wiki/List_of_colors:_N%E2%80%93Z')    
    if page != None:
        colors.extend(extract_colors(page))
    print('len', len(colors))
    colors = deduplicate_list(colors)
    print('len', len(colors))
    generate_csharp_enum(colors)

if __name__ == "__main__":
    sys.exit(int(main() or 0))
    