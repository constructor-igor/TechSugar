package com.mycompany.app.rest;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;
//import com.apress.springrecipes.court.domain.Member;

/**
 * Created by igor-z on 15-Mar-17.
 */
@Controller
public class RestMemberController {
    @RequestMapping("/members")
    public String getRestMembers(Model model) {
//// Return view membertemplate. Via resolver the view
//// will be mapped to a JAXB Marshler bound to the Member class
//        Member member = new Member();
//        member.setName("John Doe");
//        member.setPhone("1-800-800-800");
//        member.setEmail("john@doe.com");
//        model.addAttribute("member", member);
//        return "membertemplate";
        throw new NotImplementedException();
    }
}