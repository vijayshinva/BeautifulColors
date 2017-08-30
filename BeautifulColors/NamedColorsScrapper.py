from bs4 import BeautifulSoup
import requests
import sys
import os
from collections import Counter

def generate_csharp_enum(colors):
    with open('NamedColors.cs', 'w') as file:
        file.write('namespace BeautifulColors')
        file.write('{'+ os.linesep)
        file.write('public enum NamedColors')
        file.write('{'+ os.linesep)
        for name, hex in colors:
            file.write(name+' = 0x'+hex[1:]+','+os.linesep)
        file.write('}'+ os.linesep)
        file.write('}'+ os.linesep)

def deduplicate_list(data):
    seen = set()
    out = [x for x in data if x[0] not in seen and not seen.add(x[0])]
    return out

def extract_page(url):
    page = requests.get(url)
    if page.status_code == 200:
        return page.content
    else:
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

def camel_case(input):
    return ''.join(x for x in input.title() if x.isalnum())

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
    