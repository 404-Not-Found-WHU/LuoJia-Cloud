using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace TripWebLast.Pages.Account
{
    public class FAQ : PageModel
    {
        public List<FAQItem> FAQs { get; set; }

        public void OnGet()
        {
            // Ӳ����FAQ���ݣ�ʵ����Ŀ�п��Կ��Ǵ������ļ�������ȡ
            FAQs = new List<FAQItem>
            {
                new FAQItem
                {
                    Question = "�Ҹ����ע���˺ţ�",
                    Answer = "�����������'ע��'��ť����д��Ҫ��Ϣ���ύ�������ע�ᡣ"
                },
                new FAQItem
                {
                    Question = "����������ô�죿",
                    Answer = "�ڵ�¼ҳ����'��������'���ӣ����������ش�ȫ���⣬������ʾ���������������롣"
                },
                new FAQItem
                {
                    Question = "��վ�ṩ��Щ������Ϣ��",
                    Answer = "����վΪ����ʡ3A���ξ����人��ѧ�����ν�����վ��" +
                    "�����У԰�����еر꾰��"
                },
                   new FAQItem
                {
                    Question ="�ҿ����ڱ���վ��ʲô��",
                    Answer = "�ڱ���վ�������������ṩ���Ŀ�֧��ʱ�䣬���ǻ��������ʱ��滮һ�����ʵ�·�ߣ�" +
                    "ͬʱ����������ÿһ���ر꽨�����������ۣ���¿����һ���������������"
                },
                    new FAQItem
                {
                    Question ="��Ӧ��������ø���վ�滮�ҵ�·�ߣ�",
                    Answer = "��·�߹滮ҳ��������Ԥ����ʱ�����䣬���ɻ�����ǹ滮�õ�·�ߡ�"
                },
                new FAQItem
                {
                    Question = "�����ϵ�ͷ�/�����ߣ�",
                    Answer = "������ֱ�ӷ����ʼ���1102432442@qq.com��"
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