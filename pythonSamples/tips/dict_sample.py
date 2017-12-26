import copy


def update_dict(d):
    d["test"] = "2"
    return d            # can be removed from the function

if __name__ == "__main__":
    d = dict()
    d["test"] = "1"
    print("[d] before update: ", d)
    updated_d = update_dict(d)
    print("[d] after update: ", d)
    print("[updated_d]: ", d)
    new_d = copy.deepcopy(d)
    print("[new d]: ", new_d)

    print("if d is update_d: ", d is updated_d)
    print("if d is new_d: ", d is new_d)
