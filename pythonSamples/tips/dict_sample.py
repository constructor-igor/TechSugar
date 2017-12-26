import copy


def update_dic(d):
    d["test"] = "2"
    return d

if __name__ == "__main__":
    d = dict()
    d["test"] = "1"
    print("before update: ", d)
    updated_d = update_dic(d)
    print("after update: ", d)
    new_d = copy.deepcopy(d)

    print("if d is update_d: ", d is updated_d)
    print("if d is new_d: ", d is new_d)
