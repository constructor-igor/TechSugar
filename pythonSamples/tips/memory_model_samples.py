# 
# https://github.com/jeffknupp/blog/blob/master/content/2012-11-13-is-python-callbyvalue-or-callbyreference-neither.md
# 

def test1():
    print("test1")
    some_guy = 'Fred'

    first_names = []
    first_names.append(some_guy)
    print("some_guy is first_names[0]: ", some_guy is first_names[0])

    another_list_of_names = first_names
    another_list_of_names.append('George')
    some_guy = 'Bill'
    print("some_guy is first_names[0]: ", some_guy is first_names[0])

    print (some_guy, first_names, another_list_of_names)


def test2():
    print("test2")
    first_names = ['Fred', 'George', 'Bill']
    last_names = ['Smith', 'Jones', 'Williams']
    name_tuple = (first_names, last_names)

    first_names.append('Igor')
    print("name_tuple: ", name_tuple)


def test3():
    def foo(bar):
        bar.append(42)
        print(bar)    
    print("test3")
    answer_list = []
    foo(answer_list)
    print(answer_list)


def test4():
    print("test4")
    def foo(bar):
        bar = 'new value'
        print (bar)
    answer_list = 'old value'
    foo(answer_list)
    print(answer_list)

if __name__ == "__main__":
    test1()
    test2()
    test3()
    test4()