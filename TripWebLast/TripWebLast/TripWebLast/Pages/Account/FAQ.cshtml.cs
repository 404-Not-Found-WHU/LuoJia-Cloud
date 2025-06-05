using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace TripWebLast.Pages.Account
{
    public class FAQ : PageModel
    {
        public List<FAQItem> FAQs { get; set; }

        public void OnGet()
        {
            // 硬编码FAQ数据，实际项目中可以考虑从配置文件或服务获取
            FAQs = new List<FAQItem>
            {
                new FAQItem
                {
                    Question = "我该如何注册账号？",
                    Answer = "点击导航栏的'注册'按钮，填写必要信息并提交即可完成注册。"
                },
                new FAQItem
                {
                    Question = "忘记密码怎么办？",
                    Answer = "在登录页面点击'忘记密码'链接，输入绑定邮箱回答安全问题，按照提示操作即可重置密码。"
                },
                new FAQItem
                {
                    Question = "网站提供哪些景点信息？",
                    Answer = "本网站为湖北省3A旅游景区武汉大学的旅游介绍网站，" +
                    "会介绍校园内所有地标景点"
                },
                   new FAQItem
                {
                    Question ="我可以在本网站做什么？",
                    Answer = "在本网站，可以向我们提供您的可支配时间，我们会根据您的时间规划一条合适的路线，" +
                    "同时，还可以在每一个地标建筑下留言评论，和驴友们一起分享在武大的美好"
                },
                    new FAQItem
                {
                    Question ="我应该如何利用该网站规划我的路线？",
                    Answer = "在路线规划页面输入您预留的时间区间，即可获得我们规划好的路线。"
                },
                new FAQItem
                {
                    Question = "如何联系客服/开发者？",
                    Answer = "您可以直接发送邮件至1102432442@qq.com。"
                },
            };
        }
    }

    public class FAQItem
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}