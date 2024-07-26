export interface SwiperConfig {
  loop: boolean;
  speed: number;
  autoplay: {
    delay: number;
  };
  slidesPerView: string | number;
  pagination?: {
    el: string;
    type: string;
    clickable: boolean;
  };
  breakpoints: {
    [key: number]: {
      slidesPerView: number;
      spaceBetween: number;
    };
  };
}
