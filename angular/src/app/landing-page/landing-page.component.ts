import {
  AfterViewInit,
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  ElementRef,
  QueryList,
  ViewChildren,
} from '@angular/core';
import '@aarsteinmedia/dotlottie-player'
import Swiper from 'swiper';
//@ts-ignore
import PureCounter from "@srexi/purecounterjs";
import {SwiperConfig} from "./swiper-config";
import {DotsSwiperConfig} from "./dots-swiper-config";


@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  standalone: true,
  styleUrls: ['./landing-page.component.css'],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class LandingPageComponent implements AfterViewInit {
  //@ts-ignore
  @ViewChildren('swiperElement') swiperElements: QueryList<ElementRef>;

  ngAfterViewInit() {
    this.initSwiper();
    this.initSwiperTabs();
    this.initPureCounter();
  }

  initSwiper() {
    this.swiperElements.forEach((swiperElementRef: ElementRef) => {
      const swiperElement = swiperElementRef.nativeElement;
      const configElement = swiperElement.querySelector('.swiper-config');

      if (configElement) {
        let config = JSON.parse(configElement.textContent.trim());

        if (swiperElement.classList.contains('swiper-tab')) {
          this.initSwiperWithCustomPagination(swiperElement, config);
        } else {
          new Swiper(swiperElement, config);
        }
      }
    });
  }

  initSwiperWithCustomPagination(swiperElement: HTMLElement, config: any) {
    // Implement this method based on your requirements
  }


  initSwiperTabs() {
    this.swiperElements.forEach((swiperElementRef: ElementRef) => {
      const swiperElement = swiperElementRef.nativeElement;

      // Use the dotsSwiperConfig defined in the component
      let config = { ...this.getDotsSwiperConfig() };

      const dotsContainer = swiperElement.closest('section')?.querySelector('.js-custom-dots');
      if (!dotsContainer) return;

      const customDots = dotsContainer.querySelectorAll('a');

      //@ts-ignore
      const swiperInstance = new Swiper(swiperElement, config);

      swiperInstance.on('slideChange', () => {
        this.updateSwiperTabsPagination(swiperInstance, customDots);
      });

      customDots.forEach((dot:any, index:any) => {
        dot.addEventListener('click', (e: Event) => {
          e.preventDefault();
          swiperInstance.slideToLoop(index);
          this.updateSwiperTabsPagination(swiperInstance, customDots);
        });
      });

      this.updateSwiperTabsPagination(swiperInstance, customDots);
    });
  }

  updateSwiperTabsPagination(swiperInstance: Swiper, customDots: NodeListOf<Element>) {
    const activeIndex = swiperInstance.realIndex;
    customDots.forEach((dot, index) => {
      if (index === activeIndex) {
        dot.classList.add('active');
      } else {
        dot.classList.remove('active');
      }
    });
  }

  initPureCounter() {
    new PureCounter();
  }



  private getSwiperConfig():SwiperConfig{
     return {
        loop: true,
        speed: 600,
        autoplay: {
          delay: 5000
        },
        slidesPerView: "auto",
        pagination: {
          el: ".swiper-pagination",
          type: "bullets",
          clickable: true
        },
        breakpoints: {
          320: {
            slidesPerView: 1,
            spaceBetween: 40
          },
          1200: {
            slidesPerView: 1,
            spaceBetween: 1
          }
        }
      };
    }

    private getDotsSwiperConfig():DotsSwiperConfig{
      return {
        loop: true,
        speed: 600,
        autoplay: {
          delay: 5000
        },
        slidesPerView: "auto",
        breakpoints: {
          320: {
            slidesPerView: 1,
            spaceBetween: 40
          },
          1200: {
            slidesPerView: 1,
            spaceBetween: 1
          }
        }
      };
    }

}

